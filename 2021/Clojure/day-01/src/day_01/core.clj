(ns day-01.core
  (:gen-class)
  (:require [clojure.java.io :as io]))

(defn count-increases-in-depth
  "Takes a list of depth measurements and counts the number of times the depth
   increases from one measurement to the next"
  [depths]
  (->>
    (partition 2 1 depths)
    (filter #(apply < %1))
    count))

(defn count-increases-in-sum-of-depth-windows
  "Takes a window size and a list of depth measurements, partitions the
   measurements into a sliding window of measurements, sums the measurements in
   each window, and counts the number of times the value increases from one
   sum to the next"
  [window-size depths]
  (->>
   (partition window-size 1 depths)
   (map #(apply + %1))
   (partition 2 1)
   (filter #(apply < %1))
   count))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (->>
    (with-open [rdr (io/reader input-file)]
      (doall (line-seq rdr)))
    (map #(Integer/parseInt %1))))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (let [depths (read-input)]
    (println (str "Depth increased "
                  (count-increases-in-depth depths)
                  " times"))
    (println (str "Sum of depths in a sliding window of 3 increased "
                  (count-increases-in-sum-of-depth-windows 3 depths)
                  " times"))))
