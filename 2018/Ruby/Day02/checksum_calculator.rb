class ChecksumCalculator
  def calc(codes)
    code_char_counts = codes.map { |code| count_chars(code).values }
    words_with_2 = code_char_counts.select { |counts| counts.include?(2) }.count
    words_with_3 = code_char_counts.select { |counts| counts.include?(3) }.count
    words_with_2 * words_with_3
  end

  private
    def count_chars(code)
      code.chars.group_by { |c| c }.transform_values(&:count)
    end
end
