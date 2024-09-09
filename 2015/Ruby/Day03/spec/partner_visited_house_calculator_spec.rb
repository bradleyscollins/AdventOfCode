# frozen_string_literal: true

require 'rspec'
require 'partner_visited_house_calculator'

RSpec.describe PartnerVisitedHouseCalculator do
  let(:partners)         { 2 }
  let(:initial_position) { [0, 0] }

  subject do
    PartnerVisitedHouseCalculator.new(partners:         partners,
                                      initial_position: initial_position)
  end

  describe "#calculate" do
    specify { expect(subject.calculate '^v').to         eq  3 }
    specify { expect(subject.calculate '^>v<').to       eq  3 }
    specify { expect(subject.calculate '^v^v^v^v^v').to eq 11 }
  end
end
