﻿using Microsoft.AspNetCore.Http;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IFileService
    {
        public Task<ApiResult<List<Files>>> Insert(FileUploadRequest req);
        public Task<ApiResult<bool>> Delete(int idFile);
    }
}
