require_relative 'checksum_calculator'

class App
  def load_input(path)
    IO.readlines(path).map(&:strip).reject(&:empty?)
  end

  def run
    input = load_input('./input.txt')
    
    result1 = ChecksumCalculator.new.calc(input)
    puts "What is the checksum for your list of box IDs? #{result1}"

    result2 = CommonLetterFinder.new.find(input)
    puts "What letters are common between the two correct box IDs? #{result2}"
  end
end

app = App.new
app.run
