# frozen_string_literal: true

class Instruction
  attr_reader :command, :first, :last

  def initialize(command, first, last)
    @command = command
    @first = first
    @last = last
  end

  def range
    x1, y1 = first
    x2, y2 = last
    (x1..x2).flat_map { |x| (y1..y2).map { |y| [x, y] } }
  end

  def ==(other)
    self.command == other.command &&
    self.first == other.first &&
    self.last == other.last
  end

  def to_s
    "#{command} #{first} – #{last}"
  end

  def self.parse(s)
    re = /\A(?<cmd>turn on|turn off|toggle) (?<x1>\d+),(?<y1>\d+) through (?<x2>\d+),(?<y2>\d+)\Z/
    match = re.match(s)
    if match
      command =
        case match[:cmd]
        when 'turn on'  then :on
        when 'turn off' then :off
        else                 :toggle
        end

      first = [match[:x1].to_i, match[:y1].to_i]
      last = [match[:x2].to_i, match[:y2].to_i]

      Instruction.new(command, first, last)
    else
      nil
    end
  end
end
