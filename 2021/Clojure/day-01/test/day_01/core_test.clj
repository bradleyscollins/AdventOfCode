(ns day-01.core-test
  (:require [clojure.test :refer :all]
            [day-01.core :refer :all]))

(def test-depths
  '(199 200 208 210 200 207 240 269 260 263))

(deftest test-count-increases-in-depth
  (testing "Given a list of depth measurements, count-increases-in-depth counts
            the number of times a measurement is greater than the previous
            measurement"
    (is (= 7 (count-increases-in-depth test-depths)))))

(deftest test-count-increases-in-sum-of-depth-windows
  (testing "Given a window size and a list of depth measurements,
            count-increases-in-sum-of-depth-windows partitions the
            measurements into a sliding window of measurements, sums
            the measurements in each window, and counts the number of
            times the value increases from one sum to the next"
    (is (= 5 (count-increases-in-sum-of-depth-windows 3 test-depths)))))
