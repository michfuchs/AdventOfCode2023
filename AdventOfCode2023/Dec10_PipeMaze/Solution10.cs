namespace AdventOfCode2023.Dec10
{
    public class Solution10 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 10);
        private List<Data10Pipe> _pipes = new();
        private List<Data10Pipe> _mainLoop = new();
        private Data10Dir _currentDir;

        private Data10Pipe _currentPipe;

        /// <summary>
        /// Get the number of pipes in the main loop.
        /// Then divide by 2 to get furthest pipe from start.
        /// </summary>
        public long GetSolutionPartOne()
        {
            var total = 0;
            var pipesInChars = Data10.Pipes;
            // build pipes list
            for (var i = 0; i < pipesInChars.Count; i++)
            {
                var line = pipesInChars[i];
                for (var j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    _pipes.Add(new Data10Pipe(c, j, i));
                }
            }
            var startPipe = _pipes.Single(p => p.C == 'S');
            _currentPipe = startPipe;
            _currentDir = Data10Dir.Down; // either down or right, see map
            do
            {
                total++;
                SetNextPipe();
                SetNextDir();
            } while (_currentPipe != startPipe);

            return total / 2;
        }

        /// <summary>
        /// Determine where we're going next
        /// </summary>
        private void SetNextDir()
        {
            switch (_currentPipe.C)
            {
                case '|':
                case '-':
                    break; // continue in same direciton
                case 'S':
                    break; // stop
                case 'L' when _currentDir == Data10Dir.Down: _currentDir = Data10Dir.Right; break;
                case 'L' when _currentDir == Data10Dir.Left: _currentDir = Data10Dir.Up; break;
                case 'J' when _currentDir == Data10Dir.Down: _currentDir = Data10Dir.Left; break;
                case 'J' when _currentDir == Data10Dir.Right: _currentDir = Data10Dir.Up; break;
                case '7' when _currentDir == Data10Dir.Right: _currentDir = Data10Dir.Down; break;
                case '7' when _currentDir == Data10Dir.Up: _currentDir = Data10Dir.Left; break;
                case 'F' when _currentDir == Data10Dir.Left: _currentDir = Data10Dir.Down; break;
                case 'F' when _currentDir == Data10Dir.Up: _currentDir = Data10Dir.Right; break;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Walk the main pipe loop
        /// </summary>
        private void SetNextPipe()
        {
            _currentPipe.IsMainLoop = true;
            _currentPipe.Dir = _currentDir;
            _mainLoop.Add(_currentPipe);
            switch (_currentDir)
            {
                case Data10Dir.Up: _currentPipe = GetPipe(_currentPipe.X, _currentPipe.Y - 1); break;
                case Data10Dir.Right: _currentPipe = GetPipe(_currentPipe.X + 1, _currentPipe.Y); break;
                case Data10Dir.Down: _currentPipe = GetPipe(_currentPipe.X, _currentPipe.Y + 1); break;
                case Data10Dir.Left: _currentPipe = GetPipe(_currentPipe.X - 1, _currentPipe.Y); break;
                default: throw new NotImplementedException();
            }
        }

        private Data10Pipe GetPipe(int x, int y)
        {
            return _pipes.Single(p => p.X == x && p.Y == y);
        }

        /// <summary>
        /// Count the pipes which are inside the main loop
        /// </summary>
        public long GetSolutionPartTwo()
        {
            var total = 0;
            // make all non-main-loop pipes a '.'
            foreach (var pipe in _pipes)
            {
                if (!pipe.IsMainLoop)
                    pipe.C = '.';
            }

            // flood outside (to the right of moving direction - could be left but is easily verified in PrintPipes)
            foreach (var pipe in _mainLoop)
            {
                switch (pipe.C)
                {
                    case '|' when pipe.Dir == Data10Dir.Up: Flood(Data10Dir.Right, pipe); break;
                    case '|' when pipe.Dir == Data10Dir.Down: Flood(Data10Dir.Left, pipe); break;
                    case '-' when pipe.Dir == Data10Dir.Left: Flood(Data10Dir.Up, pipe); break;
                    case '-' when pipe.Dir == Data10Dir.Right: Flood(Data10Dir.Down, pipe); break;
                    case 'S': break;
                    case 'L' when pipe.Dir == Data10Dir.Up: /* inside corner */ break;
                    case 'L' when pipe.Dir == Data10Dir.Right: Flood(Data10Dir.Left, pipe); Flood(Data10Dir.Down, pipe); break;
                    case 'J' when pipe.Dir == Data10Dir.Up: Flood(Data10Dir.Right, pipe); Flood(Data10Dir.Down, pipe); break;
                    case 'J' when pipe.Dir == Data10Dir.Left: /* inside corner */ break;
                    case '7' when pipe.Dir == Data10Dir.Down: /* inside corner */ break;
                    case '7' when pipe.Dir == Data10Dir.Left: Flood(Data10Dir.Right, pipe); Flood(Data10Dir.Up, pipe); break;
                    case 'F' when pipe.Dir == Data10Dir.Down: Flood(Data10Dir.Up, pipe); Flood(Data10Dir.Left, pipe); break;
                    case 'F' when pipe.Dir == Data10Dir.Right: /* inside corner */ break;
                    default: throw new NotImplementedException();
                }
            }

            // show visual representation
            PrintPipes();

            // count 'inside' pipes
            total = _pipes.Where(p => p.C == '.').Count();
            return total;
        }

        /// <summary>
        /// Flood everything to the right of the pipe
        /// </summary>
        private void Flood(Data10Dir dir, Data10Pipe pipe)
        {
            var currentPipe = pipe;
            bool stop = false;
            do
            {
                if (dir == Data10Dir.Down && currentPipe.Y == Data10.MapHeight - 1
                    || dir == Data10Dir.Up && currentPipe.Y == 0
                    || dir == Data10Dir.Left && currentPipe.X == 0
                    || dir == Data10Dir.Right && currentPipe.X == Data10.MapWidth - 1)
                    stop = true;

                if (!stop)
                {
                    switch (dir)
                    {
                        case Data10Dir.Down: currentPipe = GetPipe(currentPipe.X, currentPipe.Y + 1); break;
                        case Data10Dir.Left: currentPipe = GetPipe(currentPipe.X - 1, currentPipe.Y); break;
                        case Data10Dir.Right: currentPipe = GetPipe(currentPipe.X + 1, currentPipe.Y); break;
                        case Data10Dir.Up: currentPipe = GetPipe(currentPipe.X, currentPipe.Y - 1); break;
                    }
                }

                if (currentPipe.IsMainLoop)
                    stop = true;
                else currentPipe.C = '0';

            } while (!stop);
        }

        private void PrintPipes()
        {
            foreach (var pipe in _pipes)
            {
                if (pipe.X == Data10.MapWidth - 1)
                    Console.WriteLine(pipe.C);
                else Console.Write(pipe.C);
            }
        }
    }
}
