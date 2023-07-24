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

    public class BaoCaoQuanTracMoiTruongKCNService : IBaoCaoQuanTracMoiTruongKCNService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public BaoCaoQuanTracMoiTruongKCNService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().FindAsync(id);
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

        public async Task<ApiResult<IList<BaoCaoQuanTracMoiTruongKCNViewModel>>> GetAll()
        {
            var result = new List<BaoCaoQuanTracMoiTruongKCNViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoQuanTracMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoQuanTracMoiTruongKCNViewModel>> GetById(int id)
        {
            var result = new BaoCaoQuanTracMoiTruongKCNViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoQuanTracMoiTruongKCN == id, include: x => x.Include(y => y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoQuanTracMoiTruongKCN, NhomTaiLieuEnum.BaoCaoQuanTracMoiTruongKCN.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoQuanTracMoiTruongKCNViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoQuanTracMoiTruongKCN>> Insert(BaoCaoQuanTracMoiTruongKCNViewModel obj)
        {
            var entity = new BaoCaoQuanTracMoiTruongKCN();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoQuanTracMoiTruongKCN;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoQuanTracMoiTruongKCN.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<BaoCaoQuanTracMoiTruongKCN>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoQuanTracMoiTruongKCNViewModel>>> GetAllPaging(BaoCaoQuanTracMoiTruongKCNRequest request)
        {
            Expression<Func<BaoCaoQuanTracMoiTruongKCN, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoQuanTracMoiTruongKCNViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoQuanTracMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoQuanTracMoiTruongKCN>> Update(BaoCaoQuanTracMoiTruongKCNViewModel obj)
        {
            var entity = new BaoCaoQuanTracMoiTruongKCN();
            entity = MapViewModelToEntity(obj);
            _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoQuanTracMoiTruongKCN;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoQuanTracMoiTruongKCN.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoQuanTracMoiTruongKCN>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoQuanTracMoiTruongKCNViewModel>>> GetListBaoCaoQuanTracMoiTruongByKCN(int idKhuCongNghiep)
        {
            var result = new List<BaoCaoQuanTracMoiTruongKCNViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoQuanTracMoiTruongKCN>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoQuanTracMoiTruongKCN, NhomTaiLieuEnum.BaoCaoQuanTracMoiTruongKCN.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoQuanTracMoiTruongKCNViewModel>>() { Data = result };
        }
        public BaoCaoQuanTracMoiTruongKCNViewModel MapEntityToViewModel(BaoCaoQuanTracMoiTruongKCN entity) {
            var result = new BaoCaoQuanTracMoiTruongKCNViewModel();
            result.IdBaoCaoQuanTracMoiTruongKCN = entity.IdBaoCaoQuanTracMoiTruongKCN;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.KhuCongNghiep.TenKhuCongNghiep;
            return result;
        }
        public BaoCaoQuanTracMoiTruongKCN MapViewModelToEntity(BaoCaoQuanTracMoiTruongKCNViewModel viewModel)
        {
            var entity = new BaoCaoQuanTracMoiTruongKCN();
            entity.IdBaoCaoQuanTracMoiTruongKCN = viewModel.IdBaoCaoQuanTracMoiTruongKCN;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;

            return entity;
        }
    }

}
