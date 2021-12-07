(ns day-06.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->fish [s]
  (map #(Integer/parseInt %1) (str/split s #",")))

(defn fish->population [fish]
  (reduce-kv #(assoc %1 %2 %3) (vec (repeat 9 0)) (frequencies fish)))

(def adult-reset-idx 6)
(defn rotate-v [v]
  (if (> 1 (count v))
    v
    (conj (vec (rest v)) (first v))))

(defn age [population]
  (let [ready-to-spawn (first population)
        rotated        (rotate-v population)]
    (assoc rotated
           adult-reset-idx
           (+ (nth rotated adult-reset-idx) ready-to-spawn))))

(defn lanternfish [population days]
  (if (zero? days)
    (apply + population)
    (lanternfish (age population) (dec days))))

(defn -main
  "Simulates a lanternfish population"
  [& args]
  (let [fish       (str->fish (first (read-input)))
        population (fish->population fish)]
    (printf "Number of fish after %d days: %d%n" 80 (lanternfish population 80))
    (printf "Number of fish after %d days: %d%n" 256 (lanternfish population 256))))
