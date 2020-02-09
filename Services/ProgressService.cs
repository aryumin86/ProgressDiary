using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DayProgress.Data;
using DayProgress.Repo;
using Microsoft.Extensions.Configuration;

namespace DayProgress.Services
{
    public class ProgressService
    {
        private readonly IConfiguration _config;
        private ProgressEntryRepo _repo;
        public ProgressService(IConfiguration config){
            _config = config;
            _repo = new ProgressEntryRepo(_config);
        }
        private static string _rawSmapleText = "Recursion is any time a function calls itself inside itself, potentially creating a infinite loop. If you’ve ever worked with canvas animations then you’ve already used recursion since we use an animate function that updates our animation before rerunning itself.";
        
        private static readonly IEnumerable<ProgressEntry> ProgressTestArray = new[]
        {
            new ProgressEntry(){
                Id = 1, Content = "AAA aaa", WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 2, Content = _rawSmapleText, WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 3, Content = "CCC ccc", WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 4, Content = _rawSmapleText, WhenCreated = DateTime.Now, Tags = new ProgressTag[]{
                    new ProgressTag(){
                        Id = 1, Title = "Tag1"
                    }
                }
            },
            new ProgressEntry(){
                Id = 5, Content = "BBB bbb", WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 6, Content = _rawSmapleText, WhenCreated = DateTime.Now, Tags = new ProgressTag[]{
                    new ProgressTag(){
                        Id = 1, Title = "Tag1"
                    }
                }
            },
            new ProgressEntry(){
                Id = 7, Content = "AAA aaa", WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 8, Content = "BBB bbb", WhenCreated = DateTime.Now, Tags = new ProgressTag[]{
                    new ProgressTag(){
                        Id = 2, Title = "Tag2"
                    }
                }
            },
            new ProgressEntry(){
                Id = 9, Content = _rawSmapleText, WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 10, Content = "AAA aaa", WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 11, Content = _rawSmapleText, WhenCreated = DateTime.Now, Tags = null
            },
            new ProgressEntry(){
                Id = 12, Content = "CCC ccc", WhenCreated = DateTime.Now, Tags = new ProgressTag[]{
                    new ProgressTag(){
                        Id = 2, Title = "Tag2"
                    }
                }
            },
        };

        public Task<IEnumerable<ProgressEntry>> GetProgressAsync()
        {
            //return Task.FromResult(ProgressTestArray);
            return Task.FromResult(_repo.GetProgressEntries(new int[0]));
        }

        public Task<IEnumerable<ProgressTag>> GetProgressTagsAsync(){
            return Task.FromResult(ProgressTestArray.SelectMany(x => x.Tags).Distinct());
        }

        public Task DeleteProgressEntryAsync(int id)
        {
            _repo.DeleteProgressEntry(id);
            return Task.FromResult(0);
        }
    }
}