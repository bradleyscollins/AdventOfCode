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

(defn check-syntax [line]
  (loop [stack '()
         code  line]
    ;; (println "stack:       " stack)
    ;; (println "code:        " code)
    (let [last-char (first stack)
          next-char (first code)]
      ;; (println "Last char (top of stack):            " last-char)
      ;; (println "Next char (in the remaining code):   " next-char)
      (cond
        (empty? code) [:ok]
        (opener? next-char) (recur (cons next-char stack) (rest code))
        :else (if (= last-char (counterpart-of next-char))
                (recur (rest stack) (rest code))
                [:corrupted {:expected (counterpart-of last-char),
                             :found     next-char}])))
    ))

(def illegal-char-scores { \) 3
                           \] 57
                           \} 1197
                           \> 25137 })

(defn result->score [[status {illegal-char :found}]]
  ;; (println "Status:      " status)
  ;; (println "Illegal char:" illegal-char)
  (case status
    :ok 0
    :corrupted (illegal-char-scores illegal-char)))

(defn score-results [results]
  (reduce + (map result->score results)))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (let [lines-of-code (read-input)
        results       (map check-syntax lines-of-code)]
    (println "Total syntax error score:" (score-results results))))
