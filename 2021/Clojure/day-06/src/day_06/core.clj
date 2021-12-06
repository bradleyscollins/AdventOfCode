(ns day-06.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->population [s]
  (->> (str/split s #",")
       (map #(Integer/parseInt %1))))

(defn ready-to-spawn? [fish]
  (zero? fish))

(defn spawn [population-before-aging]
  (let [fish-ready-to-spawn (filter ready-to-spawn? population-before-aging)]
    (repeat (count fish-ready-to-spawn) 8)))

(defn age-fish [fish]
  (if (ready-to-spawn? fish) 6 (dec fish)))

(defn age [population]
  (mapv age-fish population))

(defn lanternfish [population days]
  (if (zero? days)
    population
    (let [spawned (spawn population)
          aged    (age   population)]
      (lanternfish (into aged spawned) (dec days)))))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (let [init-population  (vec (mapcat str->population (read-input)))
        final-population (lanternfish init-population 80)]
    (println "Intial population: " init-population)
    (println "Final population:  " final-population)
    (println "Number of fish:    " (count final-population))))
