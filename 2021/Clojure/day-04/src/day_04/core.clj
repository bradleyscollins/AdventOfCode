(ns day-04.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def board-size 5)

(defn board-init [rows]
  (->> (flatten rows)
       (mapv #(vector %1 :clear))))

(defn board-mark [board number]
  (replace {[number :clear] [number :marked]} board))

(defn board-mark-all [board numbers]
  (loop [board'  board
         ns      numbers]
    (if (empty? ns)
      board'
      (recur (board-mark board' (first ns)) (rest ns)))))

(defn board-rows [board]
  (partition board-size board))

(defn board-cols [board]
  (for [n (range board-size)]
    (->> board
         (drop n)
         (partition 1 board-size)
         (apply concat))))

(defn marked? [cell]
  (= :marked (second cell)))

(defn unmarked? [cell]
  (not (marked? cell)))

(defn board-unmarked-numbers [board]
  (->> board
       (filter unmarked?)
       (map first)))

(defn board-score [board last-number-called]
  (* last-number-called
     (apply + (board-unmarked-numbers board))))

(defn all-cells-marked? [row-or-col]
  (every? marked? row-or-col))

(defn bingo? [board]
  (->> (concat (board-rows board) (board-cols board))
       (some all-cells-marked?)))

(defn game-init [boards numbers]
  {:winners        []
   :numbers-called '()
   :boards         boards
   :numbers        numbers})

(defn game-next [game]
  (let [numbers              (game :numbers)
        number-drawn         (first numbers)
        boards               (->> (game :boards)
                                  (map #(board-mark %1 number-drawn)))
        bingos               (filter bingo? boards)
        new-winners          (map (fn [brd] (vector brd number-drawn)) bingos)
        boards-still-in-play (filter (comp not bingo?) boards)]
    (assoc game
           :winners        (concat (game :winners) new-winners)
           :numbers-called (cons number-drawn (game :numbers-called))
           :boards         boards-still-in-play
           :numbers        (rest numbers))))

(defn game-finished? [game]
  (or (empty? (game :boards))
      (empty? (game :numbers))))

(defn play-game [boards numbers]
  (loop [game (game-init boards numbers)]
    (if (game-finished? game)
      game
      (recur (game-next game)))))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn line->numbers [line]
  (->> (str/split line #",")
       (map #(Integer/parseInt %1))))

(defn line->row [line]
  (->> (str/split line #" ")
       (filter not-empty)
       (map #(Integer/parseInt %1))))

(defn lines->domain [lines]
  (let [numbers (line->numbers (first lines))
        rows    (->> (rest lines)
                     (filter not-empty)
                     (map line->row))
        boards  (->> (partition board-size rows)
                     (map board-init))]
    {:numbers numbers, :boards boards}))

(defn -main
  "Advent of Code, Day 4"
  [& args]
  (let [input                      (lines->domain (read-input))
        game                       (play-game (input :boards) (input :numbers))
        [board last-number-called] (last (game :winners))]
    (println "Unmarked numbers on last winning board:    "
             (board-unmarked-numbers board))
    (println "Last number called for last winning board: " last-number-called)
    (println "Score of last winning board:               "
             (board-score board last-number-called))))
