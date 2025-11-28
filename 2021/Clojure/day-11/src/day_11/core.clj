(ns day-11.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.set     :as set]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->digits [line]
  (->> (str/split line #"")
       (map #(Integer/parseInt %1))))

(defn octopus-create [coord energy]
  [coord energy])
(def octopus-coord first)
(def octopus-energy second)
(def octopus-min-energy 0)
(def octopus-flash-energy 10)
(defn octopus-set-energy [octopus energy]
  (octopus-create (octopus-coord octopus) energy))
(defn octopus-ing-energy [octopus]
  (octopus-create (octopus-coord octopus)
                  (inc (octopus-coord octopus))))

(defn grid-init [lines]
  (let [grid (to-array-2d (map str->digits lines))
        rows (alength grid)
        cols (alength (aget grid 0))
        octopuses (for [r (range rows)
                        c (range cols)
                        :let [energy (aget grid r c)]]
                    (octopus-create [r c] energy))]
    (reduce #(assoc %1 (octopus-coord %2) %2) {} octopuses)))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (println "Hello, World!"))
