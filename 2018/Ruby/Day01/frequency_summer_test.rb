require_relative 'frequency_summer'
require 'test/unit'

class FrequencySummerTest < Test::Unit::TestCase
  def test_summer
    test_cases = [
      [ [+1, -2, +3, +1],  3 ],
      [ [+1, +1, +1],      3 ],
      [ [+1, +1, -2],      0 ],
      [ [-1, -2, -3],     -6 ],
    ]

    uut = FrequencySummer.new
    test_cases.each do |input, expected|
      assert_equal(expected, uut.calc(input))
    end
  end
end
