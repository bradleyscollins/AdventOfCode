require_relative 'checksum_calculator'
require 'test/unit'

class ChecksumCalculatorTest < Test::Unit::TestCase
  def test_calc
    input = %w(abcdef bababc abbcde abcccd aabcdd abcdee ababab)
    expected = 12
    uut = ChecksumCalculator.new
    assert_equal(expected, uut.calc(input))
  end
end
