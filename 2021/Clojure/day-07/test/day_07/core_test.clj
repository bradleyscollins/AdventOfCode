(ns day-07.core-test
  (:require [clojure.test :refer :all]
            [day-07.core :refer :all]))

(deftest median-test
  (testing "Calculates median"
    (is (= 2 (median [16 1 2 0 4 2 7 1 2 14])))))

(deftest minimize-fuel-test
  (testing "Calculates the minimum fuel required to align crabs horizontally"
    (is (= 37 (minimize-fuel [16 1 2 0 4 2 7 1 2 14])))))
