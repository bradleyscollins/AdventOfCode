(ns day-03.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(defn digitize
  "Converts a line of input into a list of individual digits"
  [input]
  (->> (str/split input #"")
       (map #(Integer/parseInt %1))))

(defn columnize
  "Converts a list of digitized inputs into columns of digits"
  [digitized-inputs]
  (apply map list digitized-inputs))

(defn histograms
  "Converts columns into histograms"
  [columns]
  (map frequencies columns))

(defn key-of-max-value
  "Gets the key of the map entry with the greatest value. If there a multiple, the last one is returned."
  [m]
  (key (apply max-key val m)))

(defn key-of-min-value
  "Gets the key of the map entry with the least value. If there a multiple, the last one is returned."
  [m]
  (key (apply min-key val m)))

(defn parse-binary [s] (Integer/parseInt s 2))

(defn calc-gamma-rate
  "Converts column histograms into a gamma rate"
  [histograms]
  (->> (map (comp str key-of-max-value) histograms)
       (str/join)
       (parse-binary)))

(defn calc-epsilon-rate
  "Converts column histograms into an epsilon rate"
  [histograms]
  (->> (map (comp str key-of-min-value) histograms)
       (str/join)
       (parse-binary)))

(defn calc-power-consumption
  "Calculates power consumption from a gamma rate & an epsilon rate"
  [gamma-rate epsilon-rate]
  (* gamma-rate epsilon-rate))

(defn calc-o₂-generator-rating
  "Calculates the O₂ generator rating from a list of inputs"
  [inputs]
  (loop [index   0
         inputs' inputs]
    (if (= 1 (count inputs'))
      ((comp parse-binary str/join first) inputs')
      (let [grouped      (group-by #(nth %1 index) inputs')
            with-0       (get grouped 0)
            with-1       (get grouped 1)
            count-with-0 (count with-0)
            count-with-1 (count with-1)
            remaining    (if (> count-with-0 count-with-1) with-0 with-1)]
        (recur (inc index) remaining)))))

(defn calc-co₂-scrubber-rating
  "Calculates the CO₂ scrubber rating from a list of inputs"
  [inputs]
  (loop [index   0
         inputs' inputs]
    (if (= 1 (count inputs'))
      ((comp parse-binary str/join first) inputs')
      (let [grouped      (group-by #(nth %1 index) inputs')
            with-0       (get grouped 0)
            with-1       (get grouped 1)
            count-with-0 (count with-0)
            count-with-1 (count with-1)
            remaining    (if (< count-with-1 count-with-0) with-1 with-0)]
        (recur (inc index) remaining)))))

(defn calc-life-support-rating
  "Calculates life support rating from a O₂ generator rating & an CO₂ scrubber rating"
  [o₂-generator-rating co₂-scrubber-rating]
  (* o₂-generator-rating co₂-scrubber-rating))

(defn -main
  "Read inputs and calculate the submarine's power consumption"
  [& args]
  (let [lines (read-input)
        inputs (map digitize lines)
        columns (columnize inputs)
        histograms (histograms columns)
        gamma-rate (calc-gamma-rate histograms)
        epsilon-rate (calc-epsilon-rate histograms)
        power-consumption (calc-power-consumption gamma-rate epsilon-rate)
        o₂-generator-rating (calc-o₂-generator-rating inputs)
        co₂-scrubber-rating (calc-co₂-scrubber-rating inputs)
        life-support-rating (calc-life-support-rating o₂-generator-rating co₂-scrubber-rating)]
    (println "Gamma rate (γ):                 " gamma-rate)
    (println "Epsilon rate (ε):               " epsilon-rate)
    (println "Power consumption (γ × ε):      " power-consumption)
    (println "O₂ generator rating:            " o₂-generator-rating)
    (println "CO₂ scrubber rating:            " co₂-scrubber-rating)
    (println "Life support rating (O₂ × CO₂): " life-support-rating)))
