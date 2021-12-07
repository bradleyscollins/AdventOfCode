(ns day-06.core-test
  (:require [clojure.test :refer :all]
            [day-06.core :refer :all]))

(def test-population (fish->population [3 4 3 1 2]))

(deftest lanternfish-test
  (testing "Simulates lanternfish population over a number of days."
      (is (= 26          (lanternfish test-population 18)))
      (is (= 5934        (lanternfish test-population 80)))
      (is (= 26984457539 (lanternfish test-population 256)))))
