(ns day-06.core-test
  (:require [clojure.test :refer :all]
            [day-06.core :refer :all]))

(deftest lanternfish-test
  (testing "Simulates lanternfish population over a number of days."
    (is (= [3 4 3 1 2]
           (lanternfish [3 4 3 1 2] 0)))
    (is (= [2 3 2 0 1]
           (lanternfish [3 4 3 1 2] 1)))
    (is (= [1 2 1 6 0 8]
           (lanternfish [3 4 3 1 2] 2)))
    (is (= [4 5 4 2 3 4 5 6 6 7]
           (lanternfish [3 4 3 1 2] 6)))
    (is (= [5 6 5 3 4 5 6 0 0 1 5 6 7 7 7 8 8]
           (lanternfish [3 4 3 1 2] 12)))
    (is (= [6 0 6 4 5 6 0 1 1 2 6 0 1 1 1 2 2 3 3 4 6 7 8 8 8 8]
           (lanternfish [3 4 3 1 2] 18)))

    (testing "Counts of latnernfish after a run."
      (is (= 26   (count (lanternfish [3 4 3 1 2] 18))))
      (is (= 5934 (count (lanternfish [3 4 3 1 2] 80)))))))
