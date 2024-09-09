# frozen_string_literal: true

require 'rspec'
require 'string_refinements'

RSpec.describe String do
  using StringRefinements

  describe "#nice?" do
    specify { expect('ugknbfddgicrmopn'.nice?) }
    specify { expect('aaa'.nice?) }

    specify { expect(!'jchzalrnumimnmhp'.nice?) }
    specify { expect(!'haegwjzuvuyypxyu'.nice?) }
    specify { expect(!'dvszwmarrgswjxmb'.nice?) }
  end

  describe "#actually_nice?" do
    specify { expect('xyxy'.actually_nice?) }
    specify { expect('aabcdefgaa'.actually_nice?) }
    specify { expect('xyx'.actually_nice?) }
    specify { expect('abcdefeghi'.actually_nice?) }
    specify { expect('aaa'.actually_nice?) }
    specify { expect('qjhvhtzxzqqjkmpb'.actually_nice?) }
    specify { expect('xxyxx'.actually_nice?) }

    specify { expect(!'uurcxstgmygtbstg'.actually_nice?) }
    specify { expect(!'ieodomkazucvgmuy'.actually_nice?) }
  end
end
