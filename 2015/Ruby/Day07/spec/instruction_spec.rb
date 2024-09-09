# frozen_string_literal: true

require 'rspec'
require 'instruction'

RSpec.describe Instruction do
  describe "#parse" do
    specify { expect(Instruction.parse('123 -> x')).to eq(Const.new(123, :x)) }
    specify { expect(Instruction.parse('456 -> y')).to eq(Const.new(456, :y)) }
    
    specify { expect(Instruction.parse('x AND y -> z')).to eq(And.new(:x, :y, :z)) }
  end
end
