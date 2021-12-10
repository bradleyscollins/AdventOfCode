(ns day-09.core-test
  (:require [clojure.test :refer :all]
            [day-09.core :refer :all]))

(deftest str->digits-test
  (testing "Converts a string of digits '1234' into their individiual digits [1 2 3 4]"
    (is (= [2 1 9 9 9 4 3 2 1 0] (str->digits "2199943210")))
    (is (= [9 8 5 6 7 8 9 8 9 2] (str->digits "9856789892")))
    (is (= [9 8 9 9 9 6 5 6 7 8] (str->digits "9899965678")))))

(deftest heightmap-init-test
  (testing "Constructing a heightmap from rows of text"
    (is (= {[0 0] [[0 0] 2]
            [0 1] [[0 1] 1]
            [0 2] [[0 2] 9]
            [0 3] [[0 3] 9]
            [0 4] [[0 4] 9]
            [0 5] [[0 5] 4]
            [0 6] [[0 6] 3]
            [0 7] [[0 7] 2]
            [0 8] [[0 8] 1]
            [0 9] [[0 9] 0]
            [1 0] [[1 0] 3]
            [1 1] [[1 1] 9]
            [1 2] [[1 2] 8]
            [1 3] [[1 3] 7]
            [1 4] [[1 4] 8]
            [1 5] [[1 5] 9]
            [1 6] [[1 6] 4]
            [1 7] [[1 7] 9]
            [1 8] [[1 8] 2]
            [1 9] [[1 9] 1]
            [2 0] [[2 0] 9]
            [2 1] [[2 1] 8]
            [2 2] [[2 2] 5]
            [2 3] [[2 3] 6]
            [2 4] [[2 4] 7]
            [2 5] [[2 5] 8]
            [2 6] [[2 6] 9]
            [2 7] [[2 7] 8]
            [2 8] [[2 8] 9]
            [2 9] [[2 9] 2]
            [3 0] [[3 0] 8]
            [3 1] [[3 1] 7]
            [3 2] [[3 2] 6]
            [3 3] [[3 3] 7]
            [3 4] [[3 4] 8]
            [3 5] [[3 5] 9]
            [3 6] [[3 6] 6]
            [3 7] [[3 7] 7]
            [3 8] [[3 8] 8]
            [3 9] [[3 9] 9]
            [4 0] [[4 0] 9]
            [4 1] [[4 1] 8]
            [4 2] [[4 2] 9]
            [4 3] [[4 3] 9]
            [4 4] [[4 4] 9]
            [4 5] [[4 5] 6]
            [4 6] [[4 6] 5]
            [4 7] [[4 7] 6]
            [4 8] [[4 8] 7]
            [4 9] [[4 9] 8]}
           (heightmap-init ["2199943210"
                            "3987894921"
                            "9856789892"
                            "8767896789"
                            "9899965678"])))))

(def test-heightmap (heightmap-init ["2199943210"
                                     "3987894921"
                                     "9856789892"
                                     "8767896789"
                                     "9899965678"]))

(deftest heightmap-points-adjacent-to-test
  (testing "Calculates the points adjacent to (ahead, behind, left, right) a point"
    (is (= [[[1 0] 3] [[0 1] 1]]
           (heightmap-points-adjacent-to test-heightmap [[0 0] 2])))
    (is (= [[[3 9] 9] [[4 8] 7]]
           (heightmap-points-adjacent-to test-heightmap [[4 9] 8])))
    (is (= [[[1 2] 8] [[3 2] 6] [[2 1] 8] [[2 3] 6]]
           (heightmap-points-adjacent-to test-heightmap [[2 2] 5])))
    ))

(deftest heightmap-low-point?-test
  (testing "Determines whether the height at a given point [row col] is a low point"
    (is (heightmap-low-point? test-heightmap [[0 1] 1]))
    (is (heightmap-low-point? test-heightmap [[0 9] 0]))
    (is (heightmap-low-point? test-heightmap [[2 2] 5]))
    (is (heightmap-low-point? test-heightmap [[4 6] 5]))
    (is (not (heightmap-low-point? test-heightmap [[0 0] 2])))))

(deftest heightmap-low-points-test
  (testing "Finds all the low points in a heightmap"
    (is (= #{[[0 1] 1]
             [[0 9] 0]
             [[2 2] 5]
             [[4 6] 5]}
           (heightmap-low-points test-heightmap)))))


(deftest heightmap-basin-from-test
  (testing "Determines the basin from a low point"
    (is (= #{[[0 0] 2]
             [[0 1] 1]
             [[1 0] 3]}
           (heightmap-basin-from test-heightmap [[0 1] 1])))))

(deftest number-of-basins-test
  (testing "Calculates the number of basins in a heightmap"
    (is (= 4 (count (heightmap-basins test-heightmap))))))

(deftest product-of-size-of-largest-three-basins-test
  (testing "Calculates the number of basins in a heightmap"
    (is (= 1134
           (->> (heightmap-basins test-heightmap)
                (map basin-size)
                (sort)
                (reverse)
                (take 3)
                (reduce *))))))
