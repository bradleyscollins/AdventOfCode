(ns day-08.core-test
  (:require [clojure.test :refer :all]
            [day-08.core :refer :all]))

(deftest str->domain-test
  (testing "Converts a line of input to a list of patterns and a list of outputs"
    (is (= [[(set "acedgfb")
             (set "cdfbe")
             (set "gcdfa")
             (set "fbcad")
             (set "dab")
             (set "cefabd")
             (set "cdfgeb")
             (set "eafb")
             (set "cagedb")
             (set "ab")]
            [(set "cdfeb") (set "fcadb") (set "cdfeb") (set "cdbaf")]]
           (str->domain "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")))))

(deftest signal-patterns->pattern-map-test
  (testing "Converts a list of signal patterns into a pattern map"
    (is (= {(set "acedgfb") 8
            (set "cdfbe")   5
            (set "gcdfa")   2
            (set "fbcad")   3
            (set "dab")     7
            (set "cefabd")  9
            (set "cdfgeb")  6
            (set "eafb")    4
            (set "cagedb")  0
            (set "ab")      1}
           (signal-patterns->pattern-map [(set "acedgfb")
                                          (set "cdfbe")
                                          (set "gcdfa")
                                          (set "fbcad")
                                          (set "dab")
                                          (set "cefabd")
                                          (set "cdfgeb")
                                          (set "eafb")
                                          (set "cagedb")
                                          (set "ab")])))))

(deftest decode-display-test
  (testing "Given a signal pattern map, decodes a list of outputs into a list of digits"
    (let [input "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"
          [patterns outputs] (str->domain input)
          pattern-map (signal-patterns->pattern-map patterns)]
      (is (= [5 3 5 3] (decode-display pattern-map outputs))))))

(deftest decode-test
  (testing "Given an input, determines the number on the display"
    (is (= 8394
           (decode "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe")))
    (is (= 9781
           (decode "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc")))
    (is (= 5353
           (decode "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")))))
