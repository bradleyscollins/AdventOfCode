# frozen_string_literal: true

require 'rspec'
require 'wrapping_paper_calculator'

RSpec.describe WrappingPaperCalculator do
  describe "#calculate" do
    specify { expect(subject.calculate '2x3x4').to  eq 58 }
    specify { expect(subject.calculate '1x1x10').to eq 43 }
  end
end
