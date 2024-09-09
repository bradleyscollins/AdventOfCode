# frozen_string_literal: true

class Instruction
  def self.parse(s)
    if match = /(?<value>\d+) -> (?<output>[a-z]+)/.match(s)
      Const.new(match[:value].to_i, match[:output])
    end
  end
end

class Const
  attr_reader :value, :output

  def initialize(value, output)
    @value = value
    @output = output
  end

  def ==(other)
    other.is_a?(self.class)     &&
    self.value  == other.value  &&
    self.output == other.output
  end

  def hash
    [self.class, value, other].hash
  end

  def to_s
    "#{value} -> #{output}"
  end

  def to_proc(circuit)
    ->() { value }
  end
end

class And
  attr_reader :left, :right, :output

  def initialize(left, right, output)
    @left = left
    @right = right
    @output = output
  end

  def ==(other)
    other.is_a?(self.class)     &&
    self.left   == other.left   &&
    self.right  == other.right  &&
    self.output == other.output
  end

  def hash
    [self.class, left, right, other].hash
  end

  def to_s
    "#{left} AND #{right} -> #{output}"
  end

  def to_proc(circuit)
    ->() { circuit[left]() && circuit[right]() }
  end
end
