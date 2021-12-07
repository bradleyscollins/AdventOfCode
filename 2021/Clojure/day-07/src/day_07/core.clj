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
       (map #(Integer/parseInt %1))))

(defn load-positions [lines]
  (mapcat str->positions lines))

(defn median [xs]
  (let [cnt (count xs)
        midpoint (quot cnt 2)
        sorted (vec (sort xs))]
    (if (odd? cnt)
      (nth sorted midpoint)
      (let [submedian   (nth sorted (dec midpoint))
            supermedian (nth sorted midpoint)]
        (quot (+ submedian supermedian) 2)))))

(defn minimize-fuel [positions]
  (let [med (median positions)]
    (->> (map #(Math/abs (- %1 med)) positions)
         (apply +))))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (let [positions (load-positions (read-input))]
    (println "Minimum fuel: " (minimize-fuel positions))))
