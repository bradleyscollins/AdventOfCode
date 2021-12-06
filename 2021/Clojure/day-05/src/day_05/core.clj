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

    :else
    (let [[step-x end-x] (if (< x₁ x₂) [1 (inc x₂)] [-1 (dec x₂)])
          [step-y end-y] (if (< y₁ y₂) [1 (inc y₂)] [-1 (dec y₂)])
          xs         (range x₁ end-x step-x)
          ys         (range y₁ end-y step-y)]
      (mapv #(vector %1 %2) xs ys))))

(defn grid-mark [grid point]
  (let [k   (point->str point)
        cnt (get grid k 0)]
       (assoc grid k (inc cnt))))

(defn grid-points-with-overlap [grid]
  (filter #(> (val %1) 1) grid))

(defn -main
  "Checks grid for overlaps"
  [& args]
  (let [lines    (map str->line (read-input))
        hv-lines (filter line-horizontal-or-vertical? lines)

        hv-points-of-interest  (mapcat line->points hv-lines)
        hv-init-grid           {}
        hv-final-grid          (reduce grid-mark hv-init-grid hv-points-of-interest)
        hv-points-with-overlap (grid-points-with-overlap hv-final-grid)

        points-of-interest  (mapcat line->points lines)
        init-grid           {}
        final-grid          (reduce grid-mark init-grid points-of-interest)
        points-with-overlap (grid-points-with-overlap final-grid)]
    
    (println "Number of points with overlap among horizontal & vertical lines: "
             (count hv-points-with-overlap))
    (println "Number of points with overlap among all lines: "
             (count points-with-overlap))))
