﻿using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Common.Enums;

namespace QuanLyMoiTruong.Application.Services
{

    public class BaoCaoQuanTracMoiTruongService : IBaoCaoQuanTracMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public BaoCaoQuanTracMoiTruongService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<BaoCaoQuanTracMoiTruongViewModel>>> GetAll()
        {
            var result = new List<BaoCaoQuanTracMoiTruongViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoQuanTracMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoQuanTracMoiTruongViewModel>> GetById(int id)
        {
            var result = new BaoCaoQuanTracMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoQuanTracMoiTruong == id, include: x => x.Include(y => y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoQuanTracMoiTruong, NhomTaiLieuEnum.BaoCaoQuanTracMoiTruong.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoQuanTracMoiTruongViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoQuanTracMoiTruong>> Insert(BaoCaoQuanTracMoiTruongViewModel obj)
        {
            var entity = new BaoCaoQuanTracMoiTruong();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoQuanTracMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoQuanTracMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<BaoCaoQuanTracMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoQuanTracMoiTruongViewModel>>> GetAllPaging(BaoCaoQuanTracMoiTruongRequest request)
        {
            Expression<Func<BaoCaoQuanTracMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoQuanTracMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoQuanTracMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoQuanTracMoiTruong>> Update(BaoCaoQuanTracMoiTruongViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoQuanTracMoiTruong == obj.IdBaoCaoQuanTracMoiTruong);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoQuanTracMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoQuanTracMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoQuanTracMoiTruong>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoQuanTracMoiTruongViewModel>>> GetListBaoCaoQuanTracMoiTruongByKCN(int idKhuCongNghiep)
        {
            var result = new List<BaoCaoQuanTracMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoQuanTracMoiTruong, NhomTaiLieuEnum.BaoCaoQuanTracMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoQuanTracMoiTruongViewModel>>() { Data = result };
        }
        public BaoCaoQuanTracMoiTruongViewModel MapEntityToViewModel(BaoCaoQuanTracMoiTruong entity) {
            var result = new BaoCaoQuanTracMoiTruongViewModel();
            result.IdBaoCaoQuanTracMoiTruong = entity.IdBaoCaoQuanTracMoiTruong;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.KhuCongNghiep.TenKhuCongNghiep;
            return result;
        }
        public BaoCaoQuanTracMoiTruong MapViewModelToEntity(BaoCaoQuanTracMoiTruongViewModel viewModel, BaoCaoQuanTracMoiTruong entity)
        {
            entity.IdBaoCaoQuanTracMoiTruong = viewModel.IdBaoCaoQuanTracMoiTruong;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;

            return entity;
        }
    }

}
