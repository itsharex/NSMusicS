package usecase_system

import (
	"context"
	"github.com/amitshekhariitbhu/go-backend-clean-architecture/domain/domain_system"
	"time"
)

type taskUsecase struct {
	taskRepository domain_system.TaskRepository
	contextTimeout time.Duration
}

func NewTaskUsecase(taskRepository domain_system.TaskRepository, timeout time.Duration) domain_system.TaskUsecase {
	return &taskUsecase{
		taskRepository: taskRepository,
		contextTimeout: timeout,
	}
}

func (tu *taskUsecase) Create(c context.Context, task *domain_system.Task) error {
	ctx, cancel := context.WithTimeout(c, tu.contextTimeout)
	defer cancel()
	return tu.taskRepository.Create(ctx, task)
}

func (tu *taskUsecase) FetchByUserID(c context.Context, userID string) ([]domain_system.Task, error) {
	ctx, cancel := context.WithTimeout(c, tu.contextTimeout)
	defer cancel()
	return tu.taskRepository.FetchByUserID(ctx, userID)
}