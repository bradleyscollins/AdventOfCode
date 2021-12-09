(ns day-09.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->digits [s]
  (map #(Integer/parseInt %1) (str/split s #"")))

(defn heightmap-init [rows]
  (to-array-2d (map str->digits rows)))

(defn heightmap-dimensions [heightmap]
  (let [rows (alength heightmap)
        cols (alength (aget heightmap 0))]
    [rows cols]))

(defn heightmap-height-at [heightmap [r c]]
  (aget heightmap r c))

(defn heightmap-points-adjacent-to [heightmap [r c]]
  (let [row-ahead         (dec r)
        row-behind        (inc r)
        col-left          (dec c)
        col-right         (inc c)
        ahead             [row-ahead  c]
        behind            [row-behind c]
        left              [r          col-left]
        right             [r          col-right]
        [row-len col-len] (heightmap-dimensions heightmap)
        adjacent-points   [ahead behind left right]]
    (letfn [(in-bounds? [[r' c']]
              (and (<= 0 r')
                   (< r' row-len)
                   (<= 0 c')
                   (< c' col-len)))]
      (filter in-bounds? adjacent-points))))

(defn heightmap-heights-adjacent-to [heightmap [r c]]
  (->> (heightmap-points-adjacent-to heightmap [r c])
       (map (partial heightmap-height-at heightmap))))

(defn heightmap-low-point? [heightmap point]
  (let [current-height (heightmap-height-at heightmap point)]
    (cond
      (zero? current-height) true
      (= 9 current-height)   false
      :else (let [adjacent-heights (heightmap-heights-adjacent-to heightmap point)]
              (every? (partial < current-height) adjacent-heights)))))

(defn heightmap-low-points [heightmap]
  (let [[rows cols] (heightmap-dimensions heightmap)
        all-points (for [r (range rows), c (range cols)] [r c])]
    (->> all-points
         (filter (partial heightmap-low-point? heightmap))
         (map (partial heightmap-height-at heightmap)))))

(defn low-point-risk-level [height]
  (inc height))

(defn -main
  "Find the low points in a cave"
  [& args]
  (let [heightmap (heightmap-init (read-input))
        low-points (heightmap-low-points heightmap)
        risk-levels (map low-point-risk-level low-points)]
    (println "Low points in the cave:" low-points)
    (println "Low point risk levels: " risk-levels)
    (println "Total risk level:      " (reduce + risk-levels))))
