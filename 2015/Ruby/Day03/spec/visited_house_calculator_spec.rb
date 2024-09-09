# frozen_string_literal: true

require 'rspec'
require 'visited_house_calculator'

RSpec.describe VisitedHouseCalculator do
  let(:initial_position) { [0, 0] }

  subject { VisitedHouseCalculator.new(initial_position) }

  describe "#calculate" do
    specify { expect(subject.calculate '>').to          eq 2 }
    specify { expect(subject.calculate '^>v<').to       eq 4 }
    specify { expect(subject.calculate '^v^v^v^v^v').to eq 2 }
  end
end
