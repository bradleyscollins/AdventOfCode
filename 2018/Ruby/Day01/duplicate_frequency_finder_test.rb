require_relative 'duplicate_frequency_finder'
require 'test/unit'

class DuplicateFrequencyFinderTest < Test::Unit::TestCase
  def test_summer
    test_cases = [
      [ [+1, -2, +3, +1],      2 ],
      [ [+1, -1],              0 ],
      [ [+3, +3, +4, -2, -4], 10 ],
      [ [-6, +3, +8, +5, -6],  5 ],
      [ [+7, +7, -2, -7, -4], 14 ],
    ]

    uut = DuplicateFrequencyFinder.new
    test_cases.each do |input, expected|
      assert_equal(expected, uut.calc(input))
    end
  end
end
