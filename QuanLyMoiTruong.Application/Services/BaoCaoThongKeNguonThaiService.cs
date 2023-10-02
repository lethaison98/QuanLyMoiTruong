using QuanLyMoiTruong.Application.Interfaces;
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
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;

namespace QuanLyMoiTruong.Application.Services
{

    public class BaoCaoThongKeNguonThaiService : IBaoCaoThongKeNguonThaiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public BaoCaoThongKeNguonThaiService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().FindAsync(id);
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

        public async Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetAll()
        {
            var result = new List<BaoCaoThongKeNguonThaiViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoThongKeNguonThaiViewModel>> GetById(int id)
        {
            var result = new BaoCaoThongKeNguonThaiViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoThongKeNguonThai == id, include: x => x.Include(y => y.DuAn).Include(y=> y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoThongKeNguonThai, NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoThongKeNguonThaiViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoThongKeNguonThai>> Insert(BaoCaoThongKeNguonThaiViewModel obj)
        {
            var entity = new BaoCaoThongKeNguonThai();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoThongKeNguonThai;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<BaoCaoThongKeNguonThai>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoThongKeNguonThaiViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<BaoCaoThongKeNguonThai, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoThongKeNguonThaiViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoThongKeNguonThai>> Update(BaoCaoThongKeNguonThaiViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoThongKeNguonThai == obj.IdBaoCaoThongKeNguonThai);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoThongKeNguonThai;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoThongKeNguonThai>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetListBaoCaoThongKeNguonThaiByDuAn(int idDuAn)
        {
            var result = new List<BaoCaoThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoThongKeNguonThai, NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }
        
        public async Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetListBaoCaoThongKeNguonThaiByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = new List<BaoCaoThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoThongKeNguonThai, NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }

        public BaoCaoThongKeNguonThaiViewModel MapEntityToViewModel(BaoCaoThongKeNguonThai entity) {
            var result = new BaoCaoThongKeNguonThaiViewModel();
            result.IdBaoCaoThongKeNguonThai = entity.IdBaoCaoThongKeNguonThai;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.IdDuAn != null? entity.DuAn.TenDuAn: "";
            result.TenDoanhNghiep = entity.IdDuAn != null ? entity.DuAn.TenDoanhNghiep : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenKhuCongNghiep : "";
            result.TenChuDauTu = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenChuDauTu : "";
            result.Nam = entity.Nam;
            result.Lan = entity.Lan;
            result.GhiChu = entity.GhiChu;
            return result;
        }
        public BaoCaoThongKeNguonThai MapViewModelToEntity(BaoCaoThongKeNguonThaiViewModel viewModel, BaoCaoThongKeNguonThai entity)
        {
            entity.IdBaoCaoThongKeNguonThai = viewModel.IdBaoCaoThongKeNguonThai;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn == 0 ? null : viewModel.IdDuAn;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep == 0 ? null : viewModel.IdKhuCongNghiep;
            entity.GhiChu = viewModel.GhiChu;
            entity.Nam = viewModel.Nam;
            entity.Lan = viewModel.Lan; 
            return entity;
        }
    }

}
