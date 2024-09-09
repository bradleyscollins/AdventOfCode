# frozen_string_literal: true

require 'rspec'
require 'advent_coin'

RSpec.describe AdventCoin do
  describe "#find_number" do
    specify { expect(subject.find_number('pqrstuv', 5)).to eq 1048970 }
    specify { expect(subject.find_number('abcdef', 5)).to  eq  609043 }
  end
end
