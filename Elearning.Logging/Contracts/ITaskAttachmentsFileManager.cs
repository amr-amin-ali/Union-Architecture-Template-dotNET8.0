namespace Elearning.Utils.Contracts;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITaskAttachmentsFileManager
{
    Task<List<string>?> SaveTaskAttachmentsFiles(IFormFileCollection? files, long taskId, bool randomizeFilesNames = false);
    void DeleteTaskAttachments(long taskId);
}
