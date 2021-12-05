;; --- Day 4: Giant Squid ---
;; https://adventofcode.com/2021/day/4
;; Input: https://adventofcode.com/2021/day/4/input
;;
;; You're already almost 1.5km (almost a mile) below the surface of the ocean,
;; already so deep that you can't see any sunlight. What you can see, however,
;; is a giant squid that has attached itself to the outside of your submarine.
;;
;; Maybe it wants to play bingo?
;;
;; Bingo is played on a set of boards each consisting of a 5x5 grid of numbers.
;; Numbers are chosen at random, and the chosen number is marked on all boards
;; on which it appears. (Numbers may not appear on all boards.) If all numbers
;; in any row or any column of a board are marked, that board wins. (Diagonals
;; don't count.)
;;
;; The submarine has a bingo subsystem to help passengers (currently, you and
;; the giant squid) pass the time. It automatically generates a random order in
;; which to draw numbers and a random set of boards (your puzzle input). For
;; example:
;;
;;     7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1
;;
;;     22 13 17 11  0
;;      8  2 23  4 24
;;     21  9 14 16  7
;;      6 10  3 18  5
;;      1 12 20 15 19
;;
;;      3 15  0  2 22
;;      9 18 13 17  5
;;     19  8  7 25 23
;;     20 11 10 24  4
;;     14 21 16 12  6
;;
;;     14 21 17 24  4
;;     10 16 15  9 19
;;     18  8 23 26 20
;;     22 11 13  6  5
;;      2  0 12  3  7
;;
;; After the first five numbers are drawn (7, 4, 9, 5, and 11), there are no
;; winners, but the boards are marked as follows (shown here adjacent to each
;; other to save space):
;;
;;     22 13  17 11*  0          3  15   0   2 22         14 21  17 24   4*
;;      8  2  23  4* 24          9* 18  13  17  5*        10 16  15  9* 19
;;     21  9* 14 16   7*        19   8   7* 25 23         18  8  23 26  20
;;      6 10   3 18   5*        20  11* 10  24  4*        22 11* 13  6   5*
;;      1 12  20 15  19         14  21  16  12  6          2  0  12  3   7*
;;
;; After the next six numbers are drawn (17, 23, 2, 0, 14, and 21), there are
;; still no winners:
;;
;;     22  13  17* 11*  0*         3  15   0*  2* 22         14* 21* 17* 24   4*
;;      8   2* 23*  4* 24          9* 18  13  17   5*        10  16  15   9* 19
;;     21*  9* 14* 16   7*        19   8   7* 25  23*        18   8  23* 26  20
;;      6  10   3  18   5*        20  11* 10  24   4*        22  11* 13   6   5*
;;      1  12  20  15  19         14* 21* 16  12   6          2*  0* 12   3   7*
;;
;; Finally, 24 is drawn:
;;
;;     22  13  17* 11*  0*         3  15   0*  2* 22         14* 21* 17* 24*  4*
;;      8   2* 23*  4* 24          9* 18  13  17   5*        10  16  15   9* 19
;;     21*  9* 14* 16   7*        19   8   7* 25  23*        18   8  23* 26  20
;;      6  10   3  18   5*        20  11* 10  24   4*        22  11* 13   6   5*
;;      1  12  20  15  19         14* 21* 16  12   6          2*  0* 12   3   7*
;;
;; At this point, the third board wins because it has at least one complete row
;; or column of marked numbers (in this case, the entire top row is marked:
;; 14 21 17 24 4).
;;
;; The score of the winning board can now be calculated. Start by finding the
;; sum of all unmarked numbers on that board; in this case, the sum is 188.
;; Then, multiply that sum by the number that was just called when the board
;; won, 24, to get the final score, 188 * 24 = 4512.
;;
;; To guarantee victory against the giant squid, figure out which board will win
;; first. What will your final score be if you choose that board?
;; ANSWER: 32844

(ns day-04.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def board-size 5)

(defn board-init [rows]
  (->> (flatten rows)
       (mapv #(vector %1 :clear))))

(defn board-row [index]
  (inc (quot index board-size)))

(defn board-col [index]
  (inc (mod index board-size)))

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

(defn all-cells-marked? [row-or-col]
  (every? marked? row-or-col))

(defn bingo? [board]
  (->> (concat (board-rows board) (board-cols board))
       (some all-cells-marked?)))

(defn game-init [boards numbers]
  {:winner         nil
   :numbers-called '()
   :boards         boards
   :numbers        numbers})

(defn game-next [game]
  (let [numbers        (game :numbers)
        boards         (game :boards)
        numbers-called (game :numbers-called)
        number-drawn (first numbers)
        updated-boards       (map #(board-mark %1 number-drawn) boards)]
    (assoc game
           :winner         (first (filter bingo? updated-boards))
           :numbers-called (cons number-drawn numbers-called)
           :boards         updated-boards
           :numbers        (rest numbers))))

(defn game-last-number-called [game]
  (first (game :numbers-called)))

(defn game-score [game]
  (let [last-number-called (game-last-number-called game)
        unmarked-numbers   (board-unmarked-numbers (game :winner))]
    (* last-number-called
       (apply + unmarked-numbers))))

(defn play-game [boards numbers]
  (loop [game (game-init boards numbers)]
    (if (game :winner)
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
  (let [input              (lines->domain (read-input))
        game               (play-game (input :boards) (input :numbers))
        winner             (game :winner)
        unmarked-numbers   (board-unmarked-numbers winner)
        last-number-called (first (game :numbers-called))]
    (println "Winning board unmarked numbers: " unmarked-numbers)
    (println "Last number called:             " last-number-called)
    (println "Winning board score:            " (game-score game))))
