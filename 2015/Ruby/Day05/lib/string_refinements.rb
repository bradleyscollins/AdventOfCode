# frozen_string_literal: true

module StringRefinements
  refine String do
    def nice?
      three_vowels = /[aeiou].*[aeiou].*[aeiou]/
      double_letter = /([a-z])\1/
      prohibited = /ab|cd|pq|xy/

      self =~ three_vowels &&
      self =~ double_letter &&
      !(self =~ prohibited)
    end

    def actually_nice?
      repeated_nonoverlapping_pair = /([a-z][a-z]).*\1/
      xyx = /([a-z])[a-z]\1/

      self =~ repeated_nonoverlapping_pair &&
      self =~ xyx
    end
  end
end
