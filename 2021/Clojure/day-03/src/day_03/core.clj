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
  "Converts a list of inputs into columns of digits"
  [inputs]
  (->> (map digitize inputs)
       (apply map list)))

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

(defn -main
  "Read inputs and calculate the submarine's power consumption"
  [& args]
  (let [inputs (read-input)
        columns (columnize inputs)
        histograms (histograms columns)
        gamma-rate (calc-gamma-rate histograms)
        epsilon-rate (calc-epsilon-rate histograms)
        power-consumption (calc-power-consumption gamma-rate epsilon-rate)]
    (println "Gamma rate (γ):            " gamma-rate)
    (println "Epsilon rate (ε):          " epsilon-rate)
    (println "Power consumption (γ × ε): " power-consumption)))
