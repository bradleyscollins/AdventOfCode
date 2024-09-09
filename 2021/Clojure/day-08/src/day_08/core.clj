(ns day-08.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.set     :as set]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn str->signal-patterns [s]
  (map set (str/split s #" ")))

(defn str->outputs [s]
  (map set (str/split s #" ")))

(defn str->domain [s]
  (let [[patterns-str outputs-str] (str/split s #" \| ")
        patterns                   (str->signal-patterns patterns-str)
        outputs                    (str->outputs outputs-str)]
    (vector patterns outputs)))

(defn decode-unique-patterns [patterns-grouped-by-length]
  (assoc {}
         (first (patterns-grouped-by-length 2)) 1
         (first (patterns-grouped-by-length 3)) 7
         (first (patterns-grouped-by-length 4)) 4
         (first (patterns-grouped-by-length 7)) 8))

(defn decode-length-5-pattern [pattern-map pattern]
  (let [num->pattern-map (set/map-invert pattern-map)]
    (case [(count (set/intersection pattern (num->pattern-map 4)))
           (count (set/intersection pattern (num->pattern-map 7)))]
      [2 2] (assoc pattern-map pattern 2)
      [3 3] (assoc pattern-map pattern 3)
      [3 2] (assoc pattern-map pattern 5)
      pattern-map)))

(defn decode-length-5-patterns [length-5-patterns pattern-map]
  (reduce decode-length-5-pattern pattern-map length-5-patterns))

(defn decode-length-6-pattern [pattern-map pattern]
  (let [num->pattern-map (set/map-invert pattern-map)
        pattern-1        (num->pattern-map 1)
        pattern-3        (num->pattern-map 3)]

    (cond
      (= 1 (count (set/intersection pattern pattern-1)))
      (assoc pattern-map pattern 6)

      (= 4 (count (set/intersection pattern pattern-3)))
      (assoc pattern-map pattern 0)

      (= 5 (count (set/intersection pattern pattern-3)))
      (assoc pattern-map pattern 9)

      :else pattern-map)))

(defn decode-length-6-patterns [length-6-patterns pattern-map]
  (reduce decode-length-6-pattern pattern-map length-6-patterns))

(defn signal-patterns->pattern-map [signal-patterns]
  (let [grouped-by-length (group-by count signal-patterns)]
    (->> (decode-unique-patterns grouped-by-length)
         (decode-length-5-patterns (grouped-by-length 5))
         (decode-length-6-patterns (grouped-by-length 6)))))

(defn decode-display [pattern-map outputs]
  (map pattern-map outputs))

(defn decode [input]
  (let [[signal-patterns outputs] (str->domain input)
        pattern-map  (signal-patterns->pattern-map signal-patterns)
        display                   (decode-display pattern-map outputs)]
    (Integer/parseInt (str/join display))))

(defn -main
  "Decodes scrambled 7-segment display"
  [& args]
  (let [lines (read-input)
        values (map decode lines)]
    (println "Values:" values)
    (println "Sum of values:" (reduce + values))))
