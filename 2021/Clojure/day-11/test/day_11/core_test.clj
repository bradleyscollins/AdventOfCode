(ns day-11.core-test
  (:require [clojure.test :refer :all]
            [day-11.core :refer :all]))

(def test-grid-1
  {:step-0 (grid-init ["11111"
                       "19991"
                       "19191"
                       "19991"
                       "11111"])
   :step-1 (grid-init ["34543"
                       "40004"
                       "50005"
                       "40004"
                       "34543"])
   :step-2 (grid-init ["45654"
                       "51115"
                       "61116"
                       "51115"
                       "45654"])})

(def test-grid-1
  {:step-0 (grid-init ["5483143223"
                       "2745854711"
                       "5264556173"
                       "6141336146"
                       "6357385478"
                       "4167524645"
                       "2176841721"
                       "6882881134"
                       "4846848554"
                       "5283751526"])})


(deftest grid-total-flashes-test
  (testing "Calculates the total number of flashes after n steps"
    (is (= 0 1))))
