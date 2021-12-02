(ns day-01.core-test
  (:require [clojure.test :refer :all]
            [day-01.core :refer :all]))

(deftest test-count-depth-increases
  (testing "Given a list of depth measurements, count-depth-increases counts the
            number of times a measurement is greater than the previous measurement"
    (is (= 7 (count-depth-increases '(199 200 208 210 200 207 240 269 260 263))))))
