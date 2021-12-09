(ns day-09.core-test
  (:require [clojure.test :refer :all]
            [day-09.core :refer :all]))

(deftest str->digits-test
  (testing "Converts a string of digits '1234' into their individiual digits [1 2 3 4]"
    (is (= [2 1 9 9 9 4 3 2 1 0] (str->digits "2199943210")))
    (is (= [9 8 5 6 7 8 9 8 9 2] (str->digits "9856789892")))
    (is (= [9 8 9 9 9 6 5 6 7 8] (str->digits "9899965678")))))

(def test-heightmap (heightmap-init ["2199943210"
                                     "3987894921"
                                     "9856789892"
                                     "8767896789"
                                     "9899965678"]))

(deftest heightmap-dimensions-test
  (testing "Gets the dimensions [rows cols] a heightmap"
    (is (= [5 10] (heightmap-dimensions test-heightmap)))))

(deftest heightmap-height-at-test
  (testing "Gets the height at a given point [row col]"
    (is (= 2 (heightmap-height-at test-heightmap [0 0])))
    (is (= 8 (heightmap-height-at test-heightmap [1 2])))
    (is (= 7 (heightmap-height-at test-heightmap [2 4])))
    (is (= 6 (heightmap-height-at test-heightmap [3 6])))
    (is (= 7 (heightmap-height-at test-heightmap [4 8])))))

(deftest heightmap-points-adjacent-to-test
  (testing "Calculates the points adjacent to (ahead, behind, left, right) the point [r c]"
    (is (= [[1 0] [0 1]] (heightmap-points-adjacent-to test-heightmap [0 0])))))

(deftest heightmap-low-point?-test
  (testing "Determines whether the height at a given point [row col] is a low point"
    (is (heightmap-low-point? test-heightmap [0 1]))
    (is (heightmap-low-point? test-heightmap [0 9]))
    (is (heightmap-low-point? test-heightmap [2 2]))
    (is (heightmap-low-point? test-heightmap [4 6]))
    (is (not (heightmap-low-point? test-heightmap [0 0])))))

(deftest heightmap-low-points-test
  (testing "Finds all the low points in a heightmap"
    (is (= [1 0 5 5] (heightmap-low-points test-heightmap)))))
