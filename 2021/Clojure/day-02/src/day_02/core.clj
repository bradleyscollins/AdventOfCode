(ns day-02.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn to-command [line]
  (let [[direction distance] (str/split line #" ")]
    [(keyword direction) (Integer/parseInt distance)]))

(defn move [[x y] [direction distance]]
  (case direction
    :forward [(+ x distance) y]
    :down    [x              (+ y distance)]
    :up      [x              (- y distance)]
    [x              y]))

(defn navigate [init-position commands]
  (reduce move init-position commands))

(defn move-2 [[x y aim] [cmd n]]
  (case cmd
    :down    [x       y               (+ aim n)]
    :up      [x       y               (- aim n)]
    :forward [(+ x n) (+ y (* aim n)) aim]
             [x       y               aim]))

(defn navigate-2 [init-state commands]
  (reduce move-2 init-state commands))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (let [lines    (read-input)
        commands (map to-command lines)]
    (let [init-position [0 0]
          [x y]         (navigate init-position commands)]
      (println "--- Part 1 ---")
      (printf "Final position is (x: %d, y: %d)%n" x y)
      (printf "Position product is %d × %d = %d%n" x y (* x y)))
    (let [init-state [0 0 0]
          [x y aim]  (navigate-2 init-state commands)]
      (newline)
      (println "--- Part 2 ---")
      (printf "Final state is (x: %d, y: %d, aim: %d)%n" x y aim)
      (printf "Position product is %d × %d = %d%n" x y (* x y)))))
