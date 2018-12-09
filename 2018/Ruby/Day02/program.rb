require_relative 'checksum_calculator'

class App
  def load_input(path)
    IO.readlines(path).map(&:strip).reject(&:empty?)
  end

  def run
    input = load_input('./input.txt')
    
    result1 = ChecksumCalculator.new.calc(input)
    puts "Result 1: #{result1}"

    result2 = '---' #DuplicateFrequencyFinder.new.calc(input)
    puts "Result 2: #{result2}"
  end
end

app = App.new
app.run
