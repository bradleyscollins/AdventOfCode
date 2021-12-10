(ns day-09.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.set     :as set]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->digits [s]
  (map #(Integer/parseInt %1) (str/split s #"")))

(def point-coord first)
(def point-height second)
(def point-risk-level (comp inc point-height))
(defn point-min-height? [point]
  (= 0 (point-height point)))
(defn point-max-height? [point]
  (= 9 (point-height point)))

(defn point-lower-than? [point other-point]
  (< (point-height point) (point-height other-point)))

(defn heightmap-init [lines]
  (let [grid (to-array-2d (map str->digits lines))
        rows (alength grid)
        cols (alength (aget grid 0))
        data (for [r (range rows)
                   c (range cols)
                   :let [height (aget grid r c)]]
               [[r c] height])]
    (reduce #(assoc %1 (first %2) %2) {} data)))

(def heightmap-points vals)

(defn heightmap-points-adjacent-to [heightmap point]
  (let [[r c]  (point-coord point)
        ahead  (heightmap [(dec r) c      ])
        behind (heightmap [(inc r) c      ])
        left   (heightmap [r       (dec c)])
        right  (heightmap [r       (inc c)])]
    (filter some? [ahead behind left right])))

(defn heightmap-low-point? [heightmap point]
  (cond
    (point-min-height? point) true
    (point-max-height? point) false
    :else (->> (heightmap-points-adjacent-to heightmap point)
               (every? (partial point-lower-than? point)))))

(defn heightmap-low-points [heightmap]
  (->> (heightmap-points heightmap)
       (filter (partial heightmap-low-point? heightmap))
       (set)))

(defn heightmap-basin-from' [basin basin-border-points heightmap]
  (if (empty? basin-border-points)
    basin
    (let [basin-with-border (set/union basin basin-border-points)
          heightmap-sans-border (reduce dissoc heightmap (map point-coord basin-border-points))
          new-border-points (->> basin-border-points
                                 (mapcat (partial heightmap-points-adjacent-to heightmap-sans-border))
                                 (filter (comp not point-max-height?))
                                 set)]
      (heightmap-basin-from' basin-with-border new-border-points heightmap-sans-border))))

(defn heightmap-basin-from [heightmap low-point]
  (heightmap-basin-from' #{} #{low-point} heightmap))

(defn heightmap-basins [heightmap]
  (->> (heightmap-low-points heightmap)
       (map (partial heightmap-basin-from heightmap))))

(defn basin-size [basin]
  (count basin))

(defn -main
  "Find the low points in a cave"
  [& args]
  (let [heightmap (heightmap-init (read-input))
        low-points (heightmap-low-points heightmap)
        risk-levels (map point-risk-level low-points)
        basins (map (partial heightmap-basin-from heightmap) low-points)
        biggest-3-basins (take 3 (reverse (sort-by basin-size basins)))]
    (println "Low points in the cave:    " low-points)
    (println "Total risk level:          " (reduce + risk-levels))
    (println "Total number of basins:    " (count basins))
    (println "Sizes of 3 biggest basins: " (map basin-size biggest-3-basins))
    (println "Product of sizes above:    " (reduce * (map basin-size biggest-3-basins)))))
