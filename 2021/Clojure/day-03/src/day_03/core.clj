;; --- Day 3: Binary Diagnostic ---
;; https://adventofcode.com/2021/day/3
;; Input: https://adventofcode.com/2021/day/3/input
;;
;; The submarine has been making some odd creaking noises, so you ask it to
;; produce a diagnostic report just in case.
;;
;; The diagnostic report (your puzzle input) consists of a list of binary
;; numbers which, when decoded properly, can tell you many useful things about
;; the conditions of the submarine. The first parameter to check is the power
;; consumption.
;;
;; You need to use the binary numbers in the diagnostic report to generate two
;; new binary numbers (called the gamma rate and the epsilon rate). The power
;; consumption can then be found by multiplying the gamma rate by the epsilon
;; rate.
;;
;; Each bit in the gamma rate can be determined by finding the most common bit
;; in the corresponding position of all numbers in the diagnostic report. For
;; example, given the following diagnostic report:
;;
;;     00100
;;     11110
;;     10110
;;     10111
;;     10101
;;     01111
;;     00111
;;     11100
;;     10000
;;     11001
;;     00010
;;     01010
;;
;; Considering only the first bit of each number, there are five 0 bits and
;; seven 1 bits. Since the most common bit is 1, the first bit of the gamma rate
;; is 1.
;;
;; The most common second bit of the numbers in the diagnostic report is 0, so
;; the second bit of the gamma rate is 0.
;;
;; The most common value of the third, fourth, and fifth bits are 1, 1, and 0,
;; respectively, and so the final three bits of the gamma rate are 110.
;;
;; So, the gamma rate is the binary number 10110, or 22 in decimal.
;;
;; The epsilon rate is calculated in a similar way; rather than use the most
;; common bit, the least common bit from each position is used. So, the epsilon
;; rate is 01001, or 9 in decimal. Multiplying the gamma rate (22) by the
;; epsilon rate (9) produces the power consumption, 198.
;;
;; Use the binary numbers in your diagnostic report to calculate the gamma rate
;; and epsilon rate, then multiply them together. What is the power consumption
;; of the submarine? (Be sure to represent your answer in decimal, not binary.)
;; ANSWER: 3901196
;;
;;
;; --- Part Two ---
;;
;; Next, you should verify the life support rating, which can be determined by
;; multiplying the oxygen generator rating by the CO2 scrubber rating.
;;
;; Both the oxygen generator rating and the CO2 scrubber rating are values that
;; can be found in your diagnostic report - finding them is the tricky part.
;; Both values are located using a similar process that involves filtering out
;; values until only one remains. Before searching for either rating value,
;; start with the full list of binary numbers from your diagnostic report and
;; consider just the first bit of those numbers. Then:
;;
;; - Keep only numbers selected by the bit criteria for the type of rating value
;;   for which you are searching. Discard numbers which do not match the bit
;;   criteria.
;; - If you only have one number left, stop; this is the rating value for which
;;   you are searching.
;; - Otherwise, repeat the process, considering the next bit to the right.
;;
;; The bit criteria depends on which type of rating value you want to find:
;;
;; - To find oxygen generator rating, determine the most common value (0 or 1)
;;   in the current bit position, and keep only numbers with that bit in that
;;   position. If 0 and 1 are equally common, keep values with a 1 in the
;;   position being considered.
;; - To find CO2 scrubber rating, determine the least common value (0 or 1) in
;;   the current bit position, and keep only numbers with that bit in that
;;   position. If 0 and 1 are equally common, keep values with a 0 in the
;;   position being considered.
;;
;; For example, to determine the oxygen generator rating value using the same
;; example diagnostic report from above:
;;
;; - Start with all 12 numbers and consider only the first bit of each number.
;;   There are more 1 bits (7) than 0 bits (5), so keep only the 7 numbers with
;;   a 1 in the first position: 11110, 10110, 10111, 10101, 11100, 10000, and
;;   11001.
;; - Then, consider the second bit of the 7 remaining numbers: there are more 0
;;   bits (4) than 1 bits (3), so keep only the 4 numbers with a 0 in the second
;;   position: 10110, 10111, 10101, and 10000.
;; - In the third position, three of the four numbers have a 1, so keep those
;;   three: 10110, 10111, and 10101.
;; - In the fourth position, two of the three numbers have a 1, so keep those
;;   two: 10110 and 10111.
;; - In the fifth position, there are an equal number of 0 bits and 1 bits (one
;;   each). So, to find the oxygen generator rating, keep the number with a 1 in
;;   that position: 10111.
;; - As there is only one number left, stop; the oxygen generator rating is
;;   10111, or 23 in decimal.
;;
;; Then, to determine the CO2 scrubber rating value from the same example above:
;;
;; - Start again with all 12 numbers and consider only the first bit of each
;;   number. There are fewer 0 bits (5) than 1 bits (7), so keep only the 5
;;   numbers with a 0 in the first position: 00100, 01111, 00111, 00010, and
;;   01010.
;; - Then, consider the second bit of the 5 remaining numbers: there are fewer 1
;;   bits (2) than 0 bits (3), so keep only the 2 numbers with a 1 in the second
;;   position: 01111 and 01010.
;; - In the third position, there are an equal number of 0 bits and 1 bits (one
;;   each). So, to find the CO2 scrubber rating, keep the number with a 0 in
;;   that position: 01010.
;; - As there is only one number left, stop; the CO2 scrubber rating is 01010,
;;   or 10 in decimal.
;;
;; Finally, to find the life support rating, multiply the oxygen generator rating (23) by the CO2 scrubber rating (10) to get 230.
;;
;; Use the binary numbers in your diagnostic report to calculate the oxygen generator rating and CO2 scrubber rating, then multiply them together. What is the life support rating of the submarine? (Be sure to represent your answer in decimal, not binary.)
;; ANSWER: 4412188

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
