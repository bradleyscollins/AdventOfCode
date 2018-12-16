require_relative 'common_letter_finder'
require 'test/unit'

class CommonLetterFinderTest < Test::Unit::TestCase
  def test_find
    input = %w(abcde fghij klmno pqrst fguij axcye wvxyz)
    expected = 'fgij'
    uut = CommonLetterFinder.new
    assert_equal(expected, uut.find(input))
  end
end
