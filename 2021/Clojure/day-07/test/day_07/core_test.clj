(ns day-07.core-test
  (:require [clojure.test :refer :all]
            [day-07.core :refer :all]))

(deftest calc-fuel-cost-test
  (testing "Calculates the fuel cost to move n steps"
    (is (= 1  (calc-fuel-cost 1)))
    (is (= 3  (calc-fuel-cost 2)))
    (is (= 6  (calc-fuel-cost 3)))
    (is (= 10 (calc-fuel-cost 4)))
    (is (= 15 (calc-fuel-cost 5)))
    (is (= 21 (calc-fuel-cost 6)))
    (is (= 45 (calc-fuel-cost 9)))
    (is (= 66 (calc-fuel-cost 11)))))

(def test-positions [16 1 2 0 4 2 7 1 2 14])

(deftest calc-total-fuel-cost-test
  (testing "Calculates the total fuel required to move all crabs to the given position"
    (is (= 168 (calc-total-fuel-cost test-positions 5)))
    (is (= 206 (calc-total-fuel-cost test-positions 2)))))

(deftest minimize-fuel-test
  (testing "Calculates the minimum fuel required to align crabs horizontally"
    (is (= 168 (minimize-fuel test-positions)))))

(deftest minimize-fuel-test
  (testing "Calculates the minimum fuel required to align crabs horizontally"
    (is (= 168 (minimize-fuel test-positions)))))
