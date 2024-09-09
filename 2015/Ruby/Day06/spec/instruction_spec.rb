# frozen_string_literal: true

require 'rspec'
require 'instruction'

RSpec.describe Instruction do
  describe "::parse" do
    specify do
      expect(Instruction.parse('turn on 0,0 through 999,999')).to eq(
        Instruction.new(:on, [0, 0], [999, 999]))

      expect(Instruction.parse('toggle 0,0 through 999,0')).to eq(
        Instruction.new(:toggle, [0, 0], [999, 0]))

      expect(Instruction.parse('turn off 499,499 through 500,500')).to eq(
        Instruction.new(:off, [499, 499], [500, 500]))
    end
  end
end
