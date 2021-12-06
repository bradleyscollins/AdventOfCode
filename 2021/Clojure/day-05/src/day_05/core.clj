(ns day-05.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->point [s]                   ; "5,9"
  (->> (str/split s #",")              ; ["5" "9"]
       (mapv #(Integer/parseInt %1)))) ; [5 9]

(defn point->str [[x y]]
  (format "%d,%d" x y))

(defn str->line [s]              ; "0,9 -> 5,9"
  (->> (str/split s #" -> ")     ; ["0,9" "5,9"]
       (mapv str->point))) ; [[0 9] [5 9]]

(defn line-vertical? [[[x₁ _y₁] [x₂ _y₂]]]
  (= x₁ x₂))

(defn line-horizontal? [[[_x₁ y₁] [_x₂ y₂]]]
  (= y₁ y₂))

(defn line-horizontal-or-vertical? [line]
  (or (line-horizontal? line)
      (line-vertical? line)))

(defn line->points [[[x₁ y₁] [x₂ y₂]]]
  (cond
    (line-vertical? [[x₁ y₁] [x₂ y₂]])
    (let [[step end] (if (< y₁ y₂) [1 (inc y₂)] [-1 (dec y₂)])
          ys         (range y₁ end step)]
      (mapv #(vector x₁ %1) ys))

    (line-horizontal? [[x₁ y₁] [x₂ y₂]])
    (let [[step end] (if (< x₁ x₂) [1 (inc x₂)] [-1 (dec x₂)])
          xs         (range x₁ end step)]
      (mapv #(vector %1 y₁) xs))

    :else []))

(defn grid-mark [grid point]
  (let [k   (point->str point)
        cnt (get grid k 0)]
       (assoc grid k (inc cnt))))

(defn grid-points-with-overlap [grid]
  (filter #(> (val %1) 1) grid))

(defn -main
  "Checks grid for overlaps"
  [& args]
  (let [lines                     (map str->line (read-input))
        horizontals-and-verticals (filter line-horizontal-or-vertical? lines)
        points-of-interest        (mapcat line->points horizontals-and-verticals)
        init-grid                 {}
        final-grid                (reduce grid-mark init-grid points-of-interest)
        points-with-overlap       (grid-points-with-overlap final-grid)]
    
    (println "Points with overlap: " points-with-overlap)
    (println "Number of points with overlap: " (count points-with-overlap))))
