(ns day-02.core-test
  (:require [clojure.test :refer :all]
            [day-02.core :refer :all]))

(deftest test-to-command
  (testing "Converts a line of input to a command"
    (is (= [:forward 5] (to-command "forward 5")))
    (is (= [:down 5]    (to-command "down 5")))
    (is (= [:forward 8] (to-command "forward 8")))
    (is (= [:up 3]      (to-command "up 3")))
    (is (= [:down 8]    (to-command "down 8")))
    (is (= [:forward 2] (to-command "forward 2")))))

(def test-commands '([:forward 5]
                     [:down    5]
                     [:forward 8]
                     [:up      3]
                     [:down    8]
                     [:forward 2]))

(deftest test-navigate
  (testing "Converts a line of input to a command"
    (is (= [15 10] (navigate [0 0] test-commands)))))
