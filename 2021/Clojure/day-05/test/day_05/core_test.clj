(ns day-05.core-test
  (:require [clojure.test :refer :all]
            [day-05.core :refer :all]))

(deftest str->point-test
  (testing "Converts a string to a pair of [x y] coordinates"
    (is (= [5 9] (str->point "5,9")))
    (is (= [9 4] (str->point "9,4")))
    (is (= [1 4] (str->point "1,4")))
    (is (= [8 2] (str->point "8,2")))))

(deftest str->line-test
  (testing "Converts a string to a line [[x₁ y₁] [x₂ y₂]]"
    (is (= [[0 9] [5 9]] (str->line "0,9 -> 5,9")))
    (is (= [[9 4] [3 4]] (str->line "9,4 -> 3,4")))
    (is (= [[6 4] [2 0]] (str->line "6,4 -> 2,0")))
    (is (= [[5 5] [8 2]] (str->line "5,5 -> 8,2")))))

(deftest line-vertical?-test
  (testing "Determines whether a line [[x₁ y₁] [x₂ y₂]] is vertical"
    (is (= false (line-vertical? [[0 9] [5 9]])))
    (is (= false (line-vertical? [[8 0] [0 8]])))
    (is (= false (line-vertical? [[9 4] [3 4]])))
    (is (= true  (line-vertical? [[2 2] [2 1]])))
    (is (= true  (line-vertical? [[7 0] [7 4]])))
    (is (= false (line-vertical? [[6 4] [2 0]])))
    (is (= false (line-vertical? [[0 9] [2 9]])))
    (is (= false (line-vertical? [[3 4] [1 4]])))
    (is (= false (line-vertical? [[0 0] [8 8]])))
    (is (= false (line-vertical? [[5 5] [8 2]])))))

(deftest line-horizontal?-test
  (testing "Determines whether a line [[x₁ y₁] [x₂ y₂]] is horizontal"
    (is (= true  (line-horizontal? [[0 9] [5 9]])))
    (is (= false (line-horizontal? [[8 0] [0 8]])))
    (is (= true  (line-horizontal? [[9 4] [3 4]])))
    (is (= false (line-horizontal? [[2 2] [2 1]])))
    (is (= false (line-horizontal? [[7 0] [7 4]])))
    (is (= false (line-horizontal? [[6 4] [2 0]])))
    (is (= true  (line-horizontal? [[0 9] [2 9]])))
    (is (= true  (line-horizontal? [[3 4] [1 4]])))
    (is (= false (line-horizontal? [[0 0] [8 8]])))
    (is (= false (line-horizontal? [[5 5] [8 2]])))))

(deftest line->points-test
  (testing "Converts a line into its constituent points."
    (testing "Verticals:"
      (is (= [[2 2] [2 1]]
             (line->points [[2 2] [2 1]])))
      (is (= [[7 0] [7 1] [7 2] [7 3] [7 4]]
             (line->points [[7 0] [7 4]]))))
    (testing "Horizontals:"
      (is (= [[0 9] [1 9] [2 9] [3 9] [4 9] [5 9]]
             (line->points [[0 9] [5 9]])))
      (is (= [[9 4] [8 4] [7 4] [6 4] [5 4] [4 4] [3 4]]
             (line->points [[9 4] [3 4]]))))
    (testing "Diagonals:"
      (is (= [[0 0] [1 1] [2 2] [3 3] [4 4] [5 5] [6 6] [7 7] [8 8]]
             (line->points [[0 0] [8 8]]))))
      (is (= [[6 4] [5 3] [4 2] [3 1] [2 0]]
             (line->points [[6 4] [2 0]]))) ))

;; (deftest grid-mark-test
;;   (testing "Marks point "))