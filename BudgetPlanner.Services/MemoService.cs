using BudgetPlanner.Data;
using BudgetPlanner.Models.Memo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class MemoService
    {
        private readonly Guid _userId;

        public MemoService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMemo(MemoCreate model)
        {
            var entity =
                new Memo()
                {
                    UserId = _userId,
                    MemoContent = model.MemoContent
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Memos.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MemoListItem> GetMemos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Memos
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new MemoListItem
                                {
                                    TransactionId = e.TransactionId,
                                    MemoContent = e.MemoContent
                                }
                               );
                return query.ToArray();
            }
        }

        public MemoDetail GetMemoById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Memos
                        .Single(e => e.TransactionId == id && e.UserId == _userId);
                return
                        new MemoDetail
                        {
                            TransactionId = entity.TransactionId,
                            MemoContent = entity.MemoContent
                        };
            }
        }

        public bool UpdateMemo(MemoEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Memos
                        .Single(e => e.TransactionId == model.TransactionId && e.UserId == _userId);

                entity.TransactionId = model.TransactionId;
                entity.MemoContent = model.MemoContent;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMemo(int memoId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Memos
                        .Single(e => e.TransactionId == memoId && e.UserId == _userId);

                ctx.Memos.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
