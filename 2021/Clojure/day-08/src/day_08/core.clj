(ns day-08.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->signal-patterns [s]
  (str/split s #" "))

(defn signal-patterns->pattern-map [signal-patterns]
  (let [grouped-by-length (group-by count signal-patterns)]
    (assoc {}
           (first (get grouped-by-length 2)) 1
           (first (get grouped-by-length 3)) 7
           (first (get grouped-by-length 4)) 4
           (first (get grouped-by-length 7)) 8)))

(def str->signal-pattern-map
  (comp signal-patterns->pattern-map str->signal-patterns))

(defn str->outputs [s]
  (str/split s #" "))

(defn str->domain [s]
  (let [[patterns-str outputs-str] (str/split s #" \| ")
        pattern-map                (str->signal-pattern-map patterns-str)
        outputs                    (str->outputs outputs-str)]
    (vector pattern-map outputs)))

(defn load-data []
  (map str->domain (read-input)))

(defn decode-output [output]
  (case (count output)
    2 1
    3 7
    4 4
    7 8
    nil))

(defn decode-display [pattern-map outputs]
  (->> (map decode-output outputs)
       (filter some?)))

(defn -main
  "Decodes scrambled 7-segment display"
  [& args]
  (let [pairs               (map str->domain (read-input))
        decoded-displays    (map #(apply decode-display %1) pairs)
        decoded-digit-count (reduce + (map count decoded-displays))]
    (println "Decoded displays:" decoded-displays)
    (println "Total number of decoded digits:" decoded-digit-count)))
