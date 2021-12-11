(ns day-10.core
  (:gen-class)
  (:require [clojure.java.io :as io]
            [clojure.set     :as set]
            [clojure.string  :as str]))

(def input-file
  (io/resource "input.txt"))

(defn read-input []
  (with-open [rdr (io/reader input-file)]
    (doall (line-seq rdr))))

(def list-of-openers (list \( \[ \{ \<))
(def list-of-closers (list \) \] \} \>))
(def counterpart-of (zipmap (concat list-of-openers list-of-closers)
                            (concat list-of-closers list-of-openers)))

(def openers (set list-of-openers))
(def closers (set list-of-closers))

(defn opener? [c]
  (contains? openers c))

(defn build-completion [unmatched-opened]
  (->> unmatched-opened
       (map (comp str counterpart-of))
       (str/join)))

(defn check-syntax [line]
  (loop [[last-opened & rest-opened    :as opened] '()
         [next-char   & remaining-code :as code]   line]
    ;; (println "opened:" opened)
    ;; (println "code:" code)
    (cond
      (and (empty? opened) (empty? code))
      [:ok]

      (empty? code)
      [:incomplete {:expected (build-completion opened)}]

      (opener? next-char)
      (recur (cons next-char opened) remaining-code)

      (= last-opened (counterpart-of next-char))
      (recur rest-opened remaining-code)

      :else
      [:corrupted {:expected (counterpart-of last-opened) :found next-char}])))

(def illegal-char-scores { \) 3
                           \] 57
                           \} 1197
                           \> 25137})

(def missing-char-scores { \) 1
                           \] 2
                           \} 3
                           \> 4 })

(defn result->score [[status {illegal-char :found, expected :expected}]]
  ;; (println "Status:      " status)
  ;; (println "Illegal char:" illegal-char)
  (case status
    :ok         0
    :corrupted  (illegal-char-scores illegal-char)
    :incomplete (reduce #(+ (* 5 %1) (missing-char-scores %2)) 0 expected)))

(def result-status first)

(defn result-corrupted? [result]
  (= :corrupted (result-status result)))

(defn result-incomplete? [result]
  (= :incomplete (result-status result)))

(defn sum-corrupted-line-scores [results]
  (->> results
       (filter result-corrupted?)
       (map result->score)
       (reduce +)))

(defn calc-median-incomplete-line-score [results]
  (let [incomplete        (filter result-incomplete? results)
        incomplete-scores (map result->score incomplete)
        midpoint          (quot (count incomplete-scores) 2)]
    (nth (sort incomplete-scores) midpoint)))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (let [lines-of-code (read-input)
        results       (map check-syntax lines-of-code)]
    (println "Total syntax error score for corrupted lines:"
             (sum-corrupted-line-scores results))
    (println "Median syntax score for incomplete lines:"
             (calc-median-incomplete-line-score results))))
