# frozen_string_literal: true

require 'rspec'
require 'ribbon_calculator'

RSpec.describe RibbonCalculator do
  describe "#calculate" do
    specify { expect(subject.calculate '2x3x4').to  eq 34 }
    specify { expect(subject.calculate '1x1x10').to eq 14 }
  end
end
