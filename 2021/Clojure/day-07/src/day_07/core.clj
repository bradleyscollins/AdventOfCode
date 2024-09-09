(ns day-07.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->positions [line]
  (->> (str/split line #",")
       (map #(Long/parseLong %1))))

(defn load-positions [lines]
  (mapcat str->positions lines))

(defn calc-fuel-cost
  ([destination current-pos]
   (let [distance (Math/abs (- current-pos destination))]
     (if (zero? distance)
       0
       (calc-fuel-cost distance))))

  ([distance]
   {:pre [(> distance 0)]}
   (quot (* distance (inc distance))
         2)))

(defn calc-total-fuel-cost [positions destination]
  (->> positions
       (map (partial calc-fuel-cost destination))
       (reduce +)))

=(defn minimize-fuel [positions]
  (let [min-pos (apply min positions)
        max-pos (apply max positions)
        domain  (range min-pos (inc max-pos))]
    (->> (map (partial calc-total-fuel-cost positions) domain)
         (apply min))))

(defn -main
  "Minimize fuel cost to align crabs horizontally"
  [& args]
  (let [positions (load-positions (read-input))]
    (println "Minimum fuel: " (minimize-fuel positions))))
